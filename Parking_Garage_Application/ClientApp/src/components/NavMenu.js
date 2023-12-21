import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import SignIn from './SignIn';
import { UserContext } from '../contexts/UserContext';

export class NavMenu extends Component {
    static displayName = NavMenu.name;
    static contextType = UserContext;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        const { user } = this.context;

        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <NavbarBrand tag={Link} to="/">Parking_Garage_Application</NavbarBrand>
                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                    <Collapse className="navbar-collapse" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav ml-auto">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            {user && Object.keys(user).length ?
                                <>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                                    </NavItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/controlGate">Control Gate</NavLink>
                                    </NavItem>
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/parkingSpotsStatus">Parking Spots</NavLink>
                                    </NavItem>
                                </>
                                :
                                <div></div>
                            }
                            {user && (
                                <NavItem>
                                    <SignIn></SignIn>
                                </NavItem>
                            )}
                        </ul>
                    </Collapse>
                </Navbar>
            </header>
        );
    }
}
