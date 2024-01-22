import React, { useState, useEffect, useContext } from 'react';
import { UserContext } from '../contexts/UserContext';
import LicensePlateInput from './licensePlateInput';

const ParkingSpotsStatus = () => {
    const { user } = useContext(UserContext);

    const [selectedSpot, setSelectedSpot] = useState(null);
    const [parkingSpots, setParkingSpots] = useState([]);
    const [dataLoaded, setDataLoaded] = useState(false);
    const [selectedCar, setSelectedCar] = useState(null);
    const [startDate, setStartDate] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [isValidDateRange, setIsValidDateRange] = useState(true);

    const handleSpotClick = (spotId) => {
        setSelectedSpot(spotId);
    };

    const handleCarSelect = (car, startDate, endDate) => {
        setSelectedCar(car);
        setStartDate(startDate);
        setEndDate(endDate);
    };

    //useEffect(() => {
    //    console.log(`Updated data: \nCar: ${selectedCar}\nStart date: ${startDate}\nEnd date: ${endDate}`);
    //}, [selectedCar, startDate, endDate]);


    const getSpots = async () => {
        let fetchedSpots = [];
        try {
            const response = await fetch(`api/spot/`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                console.error('Error fetching spots!');
                return fetchedSpots;
            }

            fetchedSpots = await response.json();
            return fetchedSpots;
        } catch (error) {
            console.error('An error occurred while fetching spots:', error);
            throw error;
        }
    };

    async function handleUpdateClick(status) {
        if (selectedSpot == null) {
            alert('Please select a parking spot.');
            return;
        }
        if (selectedCar === null && user.role === 0) {
            alert('No license plate selected.');
            return;
        }
        if (selectedCar !== null && parkingSpots.some((spot) => spot.carId === selectedCar.id)) {
            alert('Selected car already has a reservation');
            return;
        }
        if ( status === 2) {
            if (!await createReservation()) {
                setSelectedSpot(null);
                setParkingSpots(generateRealisticLayout(await getSpots()));
                return;
            }
            
        }
        await updateSpot(status);
        setSelectedSpot(null);
        setParkingSpots(generateRealisticLayout(await getSpots()));
    }

    const updateSpot = async (status) => {
        try {
            let carid = null
            if (selectedCar !== null) {
                carid = status === 0 ? null : selectedCar.id;
            }
            const response = await fetch(`api/spot/${selectedSpot}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ id: selectedSpot, status: status, carid: carid }),
            });

            console.log('Updated spot');
        } catch (error) {
            console.error('Error:', error);
        }
    };

    async function createReservation() {
        try {
            const reservationData = {
                carId: selectedCar.id,
                spotId: selectedSpot,
                startDate: startDate,
                endDate: endDate
            };
            const reservationResponse = await fetch(`/api/reservation`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(reservationData),
            });

            if (reservationResponse.ok) {
                alert('Reservation created successfully!');
                return true;
            } else {
                const errorData = await reservationResponse.json();
                alert(`Failed to create reservation: ${errorData.message}`);
            }
        } catch (error) {
            console.error('Reservation creation error:', error);
            alert('An error occurred while creating the reservation.');
        }

        return false;
    }

    const generateRealisticLayout = (fetchedParkingSpots) => {
        const numCols = 11;
        const carLanes = [1, 4]; // Set rows for the car's empty' lanes
        let spotIndex = 0;

        for (let row = 0; ; ++row) {
            for (let col = 0; col < numCols; ++col) {
                const position = { x: col, y: row };
                if (!carLanes.includes(row) || col === 0 || col === numCols - 1) {
                    fetchedParkingSpots[spotIndex++].position = position;
                }
                //else: car line -> empty
                if (spotIndex === fetchedParkingSpots.length) {
                    return fetchedParkingSpots;
                }
            }
        }
        return fetchedParkingSpots;
    };
    

    const getStatusColor = (status, role) => {
        if (role === 1) {
            switch (status) {
                case 0:
                    return 'white';
                case 1:
                    return 'blue';
                case 2:
                    return 'red';
                default:
                    return 'transparent';
            }
        }
        switch (status) {
            case 0:
                return 'white';
            case 1:
            case 2:
                return 'grey';
            default:
                return 'transparent';
        }

    };

    useEffect(() => {
        const fetchData = async () => {
            const fetchedParkingSpots = await getSpots();
            const realisticLayout = generateRealisticLayout(fetchedParkingSpots);
            setParkingSpots(realisticLayout);
            console.log(realisticLayout);
            setDataLoaded(true);
        };

        fetchData();
    }, []);

    useEffect(() => {
        if (startDate && endDate) {
            const start = new Date(startDate);
            const end = new Date(endDate);

            if (end <= start) {
                setIsValidDateRange(false);
            } else {
                setIsValidDateRange(true);
            }
        }
    }, [startDate, endDate]);

    if (!dataLoaded) {
        return <p>Loading...</p>;
    }

    // condition to set if spot is clickable based on user role and spot status
    const isClickable = (userRole, spotStatus) => {
        return (userRole === 0 && spotStatus === 0) || userRole === 1;
    };

    return (
        <div className="flex h-90 items-center justify-center">
            <div className="text-center">
                {user.role === 1
                    ? 
                        <>
                            <h1 className="text-4xl mb-6">Parking Spots Status Map</h1>
                            <h2>Select and Update Parking Spot Status:</h2>
                        </>
                    :
                        <>
                            <h1 className="text-4xl mb-6">Parking Spots Map</h1>
                            <h2>Select Parking Spot:</h2>
                        </>
                 }
                <br />
                <div style={{ display: 'grid', gridTemplateColumns: 'repeat(11, 50px)', gap: '10px' }}>

                    {parkingSpots.map((spot, index) => (
                        <div
                            key={index}
                            style={{
                                width: '50px',
                                height: '50px',
                                border: '1px solid #ccc',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                cursor: isClickable(user.role, spot.status) ? 'pointer' : 'not-allowed',
                                gridColumn: spot.position?.x !== undefined ? spot.position.x + 1 : 'auto',
                                gridRow: spot.position?.y !== undefined ? spot.position.y + 1 : 'auto',
                                backgroundColor: spot.id === selectedSpot ? '#FFD700' : getStatusColor(spot.status, user.role),
                            }}
                            onClick={() => isClickable(user.role, spot.status) && handleSpotClick(spot.id)}
                        >
                            {spot.id !== null ? spot.id : ''}
                        </div>
                    ))}

                </div>
                <div style={{ marginTop: '20px' }}>
                    {user.role === 1
                        ? 
                        <>
                                <button
                                type="button"
                                className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                                onClick={() => handleUpdateClick(0)}
                            >
                                Free
                            </button>
                            <button
                                type="button"
                                className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                                onClick={() => handleUpdateClick(1)}
                            >
                                Occupied
                            </button>
                            <button
                                type="button"
                                className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                                onClick={() => handleUpdateClick(2)}
                            >
                                Reserved
                                </button>
                        </>

                        :
                        <>
                            <LicensePlateInput onCarSelect={handleCarSelect} />
                            {!isValidDateRange && (
                                <p className="text-red-500">End date must be later than the start date.</p>
                            )}

                        <button
                            type="button"
                            disabled={!isValidDateRange}
                            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                            onClick={() => handleUpdateClick(2)}
                        >
                            Reserve Spot
                            </button>
                        </>
                    }
                </div>
            </div>
        </div>
    );
};

export default ParkingSpotsStatus;
