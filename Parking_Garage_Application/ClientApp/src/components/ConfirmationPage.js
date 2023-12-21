import React, { useState } from 'react';
import axios from 'axios';

const ConfirmationPage = ({ licensePlate }) => {
    const [confirmationStatus, setConfirmationStatus] = useState('');

    const handleConfirmPayment = async () => {
        try {
            const response = await axios.post(`/api/confirmPayment/${licensePlate}`);
            setConfirmationStatus(response.data.status);
            // Additional logic if needed
        } catch (error) {
            console.error('Confirmation failed:', error);
            setConfirmationStatus('Payment confirmation failed');
        }
    };

    return (
        <div className="flex h-90 items-center justify-center">
            <div className="text-center">
                <h1 className="text-4xl mb-6">Confirmation Page</h1>
                <p>License Plate: {licensePlate}</p>
                <button
                    type="button"
                    className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                    onClick={handleConfirmPayment}
                >
                    Confirm Payment
                </button>

                {confirmationStatus && <p>{confirmationStatus}</p>}
            </div>
        </div>
    );
};

export default ConfirmationPage;
