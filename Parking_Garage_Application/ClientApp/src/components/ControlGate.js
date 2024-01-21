import React, { useState, useContext, useEffect } from 'react';
import { UserContext } from '../contexts/UserContext';

function ControlGate() {
    const [inputValue, setInputValue] = useState('');
    const [carEntered, setCarEntered] = useState(false);
    const [buttonText, setButtonText] = useState('Enter Parking');
    const [leaveMessage, setLeaveMessage] = useState('');
    const userContext = useContext(UserContext);


    useEffect(() => {
        const urlParams = new URLSearchParams(window.location.search);
        const redirectedLicensePlate = urlParams.get('licensePlate');
        const action = urlParams.get('action');

        setInputValue(redirectedLicensePlate ? redirectedLicensePlate.toUpperCase() : '');
        setCarEntered(redirectedLicensePlate ? true : false);

        // Set the button text based on the action
        setButtonText(action === 'leave' ? 'Leave Parking' : 'Enter Parking');
    }, []);

    const handleInputChange = (e) => {
        const value = e.target.value.toUpperCase();
        setInputValue(value);
    };

    const getCarWithLicensePlate = async (licensePlate) => {
        try {
            const response = await fetch(`api/car/licenseplate/${licensePlate}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                console.error('Error fetching the car with license plate:', licensePlate);
                return null;
            }

            const car = await response.json();
            return car;
        } catch (error) {
            console.error('An error occurred while fetching the car:', error);
            throw error;
        }
    };

    const registerCarEntry = async () => {
        const { user } = userContext;

        if (!user || Object.keys(user).length === 0) {
            console.error('User not logged in');
            return;
        }

        const entryDTO = {
            userId: user.id,
            licensePlate: inputValue,
        };

        try {
            const response = await fetch('api/car/entry', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(entryDTO),
            });

            return response;
        } catch (error) {
            console.error('Error in getCarOnBarrierEntry:', error);
            throw error;
        }
    }

    const handleCarEntry = async () => {
        try {
            let carEntryResponse = await registerCarEntry();
            console.log("Car Entry Response:", carEntryResponse);

            if (!carEntryResponse) {
                console.error("Error: Car detection failed");
                return;
            }

            if (carEntryResponse.status === 404) {
                console.log("User not found");
                return;
            } else if (carEntryResponse.status === 400) {
                console.log("Car is not allowed to enter. Not enough parking spots available at the moment!");
                return;
            } else if (carEntryResponse.status === 200) {
                //let stay = await carEntryResponse.json();

                setCarEntered(true);
                setInputValue(''); 
                // TODO: Continue with further functionality e.g. associating a spot to the car...
            } else {
                console.error("Unexpected response:", carEntryResponse.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    const handleCarLeave = async () => {
        const { user } = userContext;

        if (!user || Object.keys(user).length === 0) {
            console.error('User not logged in');
            return;
        }

        const car = await getCarWithLicensePlate(inputValue);
        if (!car) {
            console.error('Error fetching the associated car');
            return;
        }

        const leaveDTO = {
            carId: car.id,
        };

        try {
            const response = await fetch('api/car/leave', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(leaveDTO),
            });

            if (!response.ok) {
                console.error('Error: Handling Car Leave:', response.status);
                return;
            }

            const stay = await response.json();
            console.log('Car left successfully:', stay);

            setCarEntered(false);
            setLeaveMessage('Car left successfully.'); // Set the leave message
            setInputValue(''); // Clear the input field
            setButtonText('Enter Parking'); // Set the button text for entering the parking
        } catch (error) {
            console.error('An error occurred while leaving the car:', error);
            // Log the entire response for more details
            console.log('Error response details:', await error.response.json());
        }
    };


    return (
        <div className="flex h-90 items-center justify-center">
            <div className="text-center">
                <h1 className="text-4xl mb-6">Control Gate Page</h1>
                <input
                    type="text"
                    placeholder="License plate..."
                    className="border p-2 mb-4"
                    value={inputValue}
                    onChange={handleInputChange}
                />
                <br />
                {leaveMessage && <p className="text-green-600">{leaveMessage}</p>}
                <button
                    type="button"
                    className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                    onClick={carEntered ? handleCarLeave : handleCarEntry}
                >
                    {buttonText}
                </button>
            </div>
        </div>
    );
}

export default ControlGate;
