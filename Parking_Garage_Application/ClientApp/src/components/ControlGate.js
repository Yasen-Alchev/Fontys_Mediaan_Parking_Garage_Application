import React, { useState, useContext } from 'react';
import { UserContext } from '../contexts/UserContext';

function ControlGate() {
    const [inputValue, setInputValue] = useState('');
    const [carEntered, setCarEntered] = useState(false);
    const userContext = useContext(UserContext);

    const handleInputChange = (e) => {
        const value = e.target.value.toUpperCase();
        setInputValue(value);
    };

    const handleButtonClick = async () => {
        try {
            const { user } = userContext;

            if (!user || Object.keys(user).length === 0) {
                console.error('User not logged in');
                return;
            }

            // Check if the car is associated with the user
            const responseCheckCar = await fetch(`api/car/${user.id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            var car = null;

            if (!responseCheckCar.ok) {

                const responseAssociateCar = await fetch('api/car', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        licensePlate: inputValue,
                        userId: user.id,
                    }),
                });

                if (!responseAssociateCar.ok) {
                    console.error('Error associating car with user');
                    return;
                }

                const responseGetCar = await fetch(`api/car/${user.id}`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });


                if (responseGetCar.ok) {
                    car = await responseGetCar.json();
                } else {
                    console.error('Error fetching the car');
                    return;
                }
            }
            else {
                car = await responseCheckCar.json();
            }

            console.log("car end res: ");
            console.log(car);


        } catch (error) {
            console.error('Error:', error);
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
                <button
                    type="button"
                    className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                    onClick={handleButtonClick}
                >
                    {carEntered ? 'Leave Parking' : 'Enter Parking'}
                </button>
            </div>
        </div>
    );
}

export default ControlGate;
