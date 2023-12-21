import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import axios from 'axios';

const PaymentPage = () => {
    const [licensePlate, setLicensePlate] = useState('');
    const [paymentAmount, setPaymentAmount] = useState(null);
    const [error, setError] = useState(null);

    const handleLicensePlateChange = (event) => {
        setLicensePlate(event.target.value);
    };

    const simulatePayment = async () => {
        try {
            // Replace 'YOUR_API_BASE_URL' and 'YOUR_CAR_ID' with actual values
            const response = await axios.get(`YOUR_API_BASE_URL/api/Payment/YOUR_CAR_ID/calculate?licensePlate=${licensePlate}`);
            setPaymentAmount(response.data);
            setError(null);
        } catch (error) {
            setError('Failed to simulate payment. Please try again.');
            setPaymentAmount(null);
        }
    };

    return (
        <div>
            <h2>Payment Page</h2>
            <Form>
                <FormGroup>
                    <Label for="licensePlate">License Plate</Label>
                    <Input
                        type="text"
                        name="licensePlate"
                        id="licensePlate"
                        placeholder="Enter license plate"
                        value={licensePlate}
                        onChange={handleLicensePlateChange}
                    />
                </FormGroup>
                <Button color="primary" onClick={simulatePayment}>
                    Simulate Payment
                </Button>
            </Form>

            {paymentAmount !== null && (
                <div>
                    <h4>Payment Details</h4>
                    <p>Entry Time: {/* Include entry time details here */}</p>
                    <p>Hours Spent: {/* Include hours spent details here */}</p>
                    <p>Price: ${paymentAmount.toFixed(2)}</p>
                    <Alert color="success">Payment successful!</Alert>
                </div>
            )}

            {error && <Alert color="danger">{error}</Alert>}
        </div>
    );
};

export default PaymentPage;
