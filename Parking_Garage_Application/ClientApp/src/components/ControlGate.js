import React, { useState } from 'react';

function ControlGate() {
    const [inputValue, setInputValue] = useState('');

    const handleInputChange = (e) => {
        const value = e.target.value.toUpperCase();
        setInputValue(value);
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
                <button type="button" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">Open/Close Gate</button>
            </div>
        </div>
    );
}

export default ControlGate;
