import React, { useState, useContext, useEffect } from 'react';
import { UserContext } from '../contexts/UserContext';
import { NavLink } from "react-router-dom";

const LicensePlateInput = ({ onCarSelect }) => {
    const userContext = useContext(UserContext);
    const [inputValue, setInputValue] = useState('');
    const [selectedCar, setSelectedCar] = useState(null); // Change state to store selected car
    const [selectedStartDate, setSelectedStartDate] = useState(null); // Change state to store reservateion start date
    const [selectedEndDate, setSelectedEndDate] = useState(null); // Change state to store reservateion end date
    const [userCars, setUserCars] = useState([]); // Store all user cars
    const [price, setPrice] = useState(0); // Stores estimated price

    useEffect(() => {
        const loadLicensePlates = async () => {
            try {
                const fetchedUserCars = await fetchLicensePlates();

                if (fetchedUserCars) {
                    setUserCars(fetchedUserCars);
                }
            } catch (error) {
                console.error('An error occurred while loading license plates:', error);
            }
        };

        loadLicensePlates();
    }, [userContext]);

    const fetchLicensePlates = async () => {
        const { user } = userContext;

        if (!user || Object.keys(user).length === 0) {
            console.error('User not logged in');
            return null;
        }

        try {
            const response = await fetch('api/car', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                console.error('Error fetching cars');
                return null;
            }

            const cars = await response.json();
            const userCars = cars.filter((car) => car.userId === user.id);

            return userCars;
        } catch (error) {
            console.error('An error occurred while fetching the cars:', error);
            throw error;
        }
    };

    const handleSelectChange = (e) => {
        const selectedPlate = e.target.value;
        const selectedCar = userCars.find((car) => car.licensePlate === selectedPlate);
        setSelectedCar(selectedCar);
    };

    const handleStartDateChange = (e) => {
        setSelectedStartDate(e.target.value);
    };

    const handleEndDateChange = (e) => {
        setSelectedEndDate(e.target.value);
    };

    useEffect(() => {
        if (selectedCar && selectedStartDate && selectedEndDate) {
            onCarSelect(selectedCar, selectedStartDate, selectedEndDate);
        }
    }, [selectedCar, selectedStartDate, selectedEndDate]);

    const estimatePrice = async (entryTime, leaveTime, licensePlate) => {
        try {
            const response = await fetch(`/api/payment/estimate?entryTime=${entryTime.toISOString()}&leaveTime=${leaveTime.toISOString()}&licensePlate=${encodeURIComponent(licensePlate)}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const estimatedPrice = await response.json();
            const price = estimatedPrice > 0 ? estimatedPrice : 0;
            setPrice(price);
        } catch (error) {
            console.error('Error estimating price:', error);
        }
    };



    useEffect(() => {
        if (selectedCar && selectedStartDate && selectedEndDate) {
            estimatePrice(new Date(selectedStartDate), new Date(selectedEndDate), selectedCar.licensePlate);
        }
    }, [selectedCar, selectedStartDate, selectedEndDate]);

    

    return (
        <div className="my-4">
            <label htmlFor="licensePlate" className="block text-sm font-medium text-gray-700">
                Select License Plate:
            </label>
            <select
                id="licensePlate"
                value={selectedCar ? selectedCar.licensePlate : ''}
                onChange={handleSelectChange}
                className="mt-1 p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:border-blue-300 sm:text-sm"
            >
                <option value="" disabled>
                    Select a license plate
                </option>
                {userCars.map((car, index) => (
                    <option key={index} value={car.licensePlate}>
                        {car.licensePlate}
                    </option>
                ))}
            </select>
            <br /><p className="block text-sm font-medium text-gray-700">Don't see your license plate? Click <NavLink to="/CarRegistration" className="underline">here</NavLink> to register your car!</p>

            <div className="mt-4">
                <label htmlFor="startDate" className="block text-sm font-medium text-gray-700">
                    Start Date:
                </label>
                <input
                    type="date"
                    id="startDate"
                    name="startDate"
                    className="mt-1 p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:border-blue-300 sm:text-sm"
                    // Assuming you have a state or function to handle this
                    onChange={handleStartDateChange}
                />
            </div>

            <div className="mt-4">
                <label htmlFor="endDate" className="block text-sm font-medium text-gray-700">
                    End Date:
                </label>
                <input
                    type="date"
                    id="endDate"
                    name="endDate"
                    className="mt-1 p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:border-blue-300 sm:text-sm"
                    // Assuming you have a state or function to handle this
                    onChange={handleEndDateChange}
                />

                <div className="mt-4">
                    <label htmlFor="price" className="block text-sm font-medium text-gray-700">
                        Price:
                    </label>
                    <input
                        type="text"
                        id="price"
                        name="price"
                        value={`${price} \u20AC`}
                        readOnly
                        className="mt-1 p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:border-blue-300 sm:text-sm"
                    />
                </div>
            </div>
        </div>
    );
};

export default LicensePlateInput;
