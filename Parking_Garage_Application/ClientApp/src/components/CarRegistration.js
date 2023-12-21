import React, { useState, useContext, useEffect } from 'react';
import { UserContext } from '../contexts/UserContext';
import { useNavigate } from 'react-router-dom';

const CarRegistration = () => {
    const navigate = useNavigate();
    const [inputValue, setInputValue] = useState('');
    const userContext = useContext(UserContext);

    const handleInputChange = (e) => {
        const value = e.target.value.toUpperCase();
        setInputValue(value);
    };

    const onRegisterClick = async () => {
        const { user } = userContext;

        if (!user || Object.keys(user).length === 0) {
            console.error('User not logged in');
            return;
        }
        if (inputValue.length < 2) {
            console.error('Invalid license plate');
        }

        const carDTO = {
            licensePlate: inputValue,
            userId: user.id,
        };

        try {
            const response = await fetch('api/car', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(carDTO),
            });

            alert("Succesfully registered car");
            navigate(-1);
            return response;
        } catch (error) {
            console.error('Error in getCarOnBarrierEntry:', error);
            throw error;
        }
    }

    return (
        <div className="flex h-90 items-center justify-center">
            <div className="text-center">
                <h1 className="text-4xl mb-6">Car Registration</h1>
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
                    onClick={onRegisterClick}
                >
                    Register Car
                </button>
            </div>
        </div>
    );
}

export default CarRegistration;