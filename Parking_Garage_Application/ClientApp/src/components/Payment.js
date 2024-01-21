import React, { useState, useEffect, useContext } from 'react';
import { UserContext } from '../contexts/UserContext';

const Payment = () => {
    const { user } = useContext(UserContext);
    const [cars, setCars] = useState([]);
    const [selectedCar, setSelectedCar] = useState(null);
    const [totalCost, setTotalCost] = useState(null);
    const [paymentStatus, setPaymentStatus] = useState(null);

    useEffect(() => {
        // Fetch user's cars when the component mounts
        const fetchCars = async () => {
            try {
                const response = await fetch(`/api/car/user/${user.id}`);
                const data = await response.json();
                setCars(data);

                // If there's only one car, set it as the selected car
                if (data.length === 1) {
                    setSelectedCar(data[0]);
                }
            } catch (error) {
                console.error('Error fetching cars:', error);
            }
        };

        fetchCars();
    }, [user.id]);

    const calculateCosts = async () => {
        if (selectedCar) {
            try {
                const response = await fetch(`/api/price/payment/${selectedCar.licensePlate}`);

                if (!response.ok) {
                    throw new Error(`Request failed with status: ${response.status}`);
                }

                const cost = await response.json();
                setTotalCost(cost);
            } catch (error) {
                console.error('Error calculating costs:', error.message);
            }
        }
    };

    const payTotal = async () => {
        // You need to implement your payment logic here
        // For demonstration purposes, let's assume payment is successful if totalCost is not null
        const paymentSuccess = totalCost !== null;

        if (paymentSuccess) {
            setPaymentStatus('Payment was successful.');

            // Redirect to ControlGate with the selected car's license plate as a query parameter
            const redirectUrl = `/controlGate?licensePlate=${selectedCar.licensePlate}&action=leave`;
            setTimeout(() => {
                window.location.replace(redirectUrl);
            }, 2000);
        } else {
            setPaymentStatus('Payment was not successful.');
        }
    };



    return (
        <div className="flex items-center justify-center bg-gray-100">
            <div className="container mx-auto p-4 mt-8">
                <h1 className="text-2xl font-semibold mb-4">Payment Details</h1>

                {/* Display Price */}
                <p className="mb-4">Amount to be paid: ${totalCost !== null ? totalCost.toFixed(2) : 'XX.XX'}</p>

                {/* Display Payment Status */}
                {paymentStatus && <p className={paymentStatus.includes('successful') ? 'text-green-600' : 'text-red-600'}>{paymentStatus}</p>}

                {/* Dropdown for Multiple Cars (Conditionally Displayed) */}
                {cars.length > 1 && (
                    <div className="mb-4">
                        <label htmlFor="carDropdown" className="block text-sm font-medium text-gray-700">Select Car:</label>
                        <select
                            id="carDropdown"
                            name="carDropdown"
                            className="mt-1 p-2 border rounded-md"
                            value={selectedCar ? selectedCar.id : ''}
                            onChange={(e) => {
                                const selectedId = parseInt(e.target.value);
                                const selectedCar = cars.find((car) => car.id === selectedId);
                                setSelectedCar(selectedCar);
                                setTotalCost(null); // Reset total cost when selecting a new car
                                setPaymentStatus(null); // Reset payment status when selecting a new car
                            }}
                        >
                            {cars.map((car) => (
                                <option key={car.id} value={car.id}>
                                    {car.licensePlate}
                                </option>
                            ))}
                        </select>
                    </div>
                )}

                {/* Pay Button */}
                <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600" onClick={() => calculateCosts()}>
                    Calculate Stay Costs
                </button>
                <button className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600" onClick={() => payTotal()}>
                    Pay Total
                </button>
            </div>
        </div>
    );
}

export default Payment;
