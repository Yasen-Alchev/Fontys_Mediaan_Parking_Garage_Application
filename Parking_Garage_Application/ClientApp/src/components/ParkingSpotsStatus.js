import React, { useState, useContext } from 'react';
import { UserContext } from '../contexts/UserContext';

const ParkingSpotsStatus = () => {
    const userContext = useContext(UserContext);

    const [selectedSpot, setSelectedSpot] = useState(null);

    const handleSpotClick = (spotId) => {
        setSelectedSpot(spotId);
    };

    const markSpotAsFree = () => {
        if (selectedSpot !== null) {
            console.log(`Selected Spot: ${selectedSpot}, Status: Free`);
            setSelectedSpot(null);
        } else {
            alert('Please select a parking spot.');
        }
    };

    const markSpotAsOccupied = () => {
        if (selectedSpot !== null) {
            console.log(`Selected Spot: ${selectedSpot}, Status: Occupied`);
            setSelectedSpot(null);
        } else {
            alert('Please select a parking spot.');
        }
    };

    const markSpotAsReserved = () => {
        if (selectedSpot !== null) {
            console.log(`Selected Spot: ${selectedSpot}, Status: Reserved`);
            setSelectedSpot(null);
        } else {
            alert('Please select a parking spot.');
        }
    };

    const generateRealisticLayout = () => {
        const numRows = 6;
        const numCols = 10; 
        const parkingSpots = [];
        const carLanes = [1, 4]; // Set rows for the car lanes
        let spotId = 1;

        for (let row = 0; row < numRows; row++) {
            for (let col = 0; col < numCols; col++) {
                const position = { x: col, y: row };
                if (carLanes.includes(row)) {
                    if (col === 0 || col === numCols - 1) {
                        parkingSpots.push({ id: spotId++, position });
                    } else if (col % 2 !== 0) {
                        col += 1;
                    }
                } else {
                    parkingSpots.push({ id: spotId++, position });
                }
            }
        }

        return parkingSpots;
    };

    const parkingSpots = generateRealisticLayout();

    return (
        <div className="flex h-90 items-center justify-center">
            <div className="text-center">
                <h1 className="text-4xl mb-6">Parking Spots Status Map</h1>
                <h2>Select and Update Parking Spot Status:</h2>
                <br />
                <div style={{ display: 'grid', gridTemplateColumns: 'repeat(11, 50px)', gap: '10px' }}>
                    {parkingSpots.map((spot) => (
                        <div
                            key={spot.id}
                            style={{
                                width: '50px',
                                height: '50px',
                                border: '1px solid #ccc',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                cursor: 'pointer',
                                gridColumn: spot.position.x + 1,
                                gridRow: spot.position.y + 1,
                                backgroundColor: spot.id === selectedSpot ? '#FFD700' : 'transparent',
                            }}
                            onClick={() => handleSpotClick(spot.id)}
                        >
                            {spot.id}
                        </div>
                    ))}
                </div>
                <div style={{ marginTop: '20px' }}>
                    <button
                        type="button"
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                        onClick={markSpotAsFree}
                    >
                        Free
                    </button>
                    <button
                        type="button"
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                        onClick={markSpotAsOccupied}
                    >
                        Occupied
                    </button>
                    <button
                        type="button"
                        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                        onClick={markSpotAsReserved}
                    >
                        Reserved
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ParkingSpotsStatus;
