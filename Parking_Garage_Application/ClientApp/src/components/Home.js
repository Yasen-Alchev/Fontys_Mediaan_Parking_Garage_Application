import React, { Component } from 'react';
import { UserContext } from '../contexts/UserContext';
import 'bootstrap/dist/css/bootstrap.min.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCar, faBuilding, faCalendarAlt, faMobileAlt, faBriefcase, faShieldAlt } from '@fortawesome/free-solid-svg-icons';

export class Home extends Component {
    static contextType = UserContext;
    static displayName = Home.name;

    render() {
    return (
        <div className="container my-5">
            <h1 className="text-center mb-4">Welcome to Median Parking Solutions – Where Parking Meets Perfection!</h1>

            <div className="text-center mb-5">
                <p><strong>Efficient Parking for Everyone – Individuals and Businesses Alike</strong></p>
                <p>At Median, we believe in transforming the conventional parking experience into a seamless, efficient, and hassle-free endeavor. Whether you're an individual seeking a spot for your vehicle or a business in need of comprehensive parking solutions, Median is your ultimate parking partner.</p>
            </div>

            <div className="row">
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faCar} size="3x" />
                    <h3>Diverse Parking Options</h3>
                    <p>From convenient street parking to secure parking lots, Median caters to all your parking needs.</p>
                </div>
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faBuilding} size="3x" />
                    <h3>Corporate Solutions</h3>
                    <p>We offer tailored parking solutions for businesses of all sizes, ensuring your employees and clients always have a spot.</p>
                </div>
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faCalendarAlt} size="3x" />
                    <h3>Reserve Your Spot</h3>
                    <p>Say goodbye to parking anxieties. With Median, you can effortlessly reserve a parking space ahead of time, guaranteeing peace of mind.</p>
                </div>
            </div>

            <div className="row">
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faMobileAlt} size="3x" />
                    <h3>User-Friendly App</h3>
                    <p>Our intuitive web app makes finding and booking a parking spot an easy task.</p>
                </div>
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faBriefcase} size="3x" />
                    <h3>Bespoke Services for Companies</h3>
                    <p>Exclusive corporate accounts, bulk booking discounts, and customized parking management services.</p>
                </div>
                <div className="col-md-4 mb-3 text-center">
                    <FontAwesomeIcon icon={faShieldAlt} size="3x" />
                    <h3>Safe & Secure</h3>
                    <p>Your vehicle's safety is our priority. Enjoy peace of mind with our secure and well-maintained parking facilities.</p>
                </div>
            </div>

            <footer className="text-center mt-5">
                <p>Median Parking Solutions – Your Journey, Our Spaces! 🌟</p>
            </footer>
        </div>
    );
  }
}


