import React, { Component } from 'react';
import {
    Collapse,
    Navbar,
    NavItem,
    NavLink
} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            isAuthenticated: localStorage.getItem('isAuthenticated') === 'true'
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    handleLogout = () => {
        localStorage.setItem('isAuthenticated', 'false');
        localStorage.setItem('token', '');
        this.setState({ isAuthenticated: false });
        window.location.href = '/login';
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            {!this.state.isAuthenticated ? (
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                                </NavItem>
                            ) : (
                                <NavItem>
                                    <NavLink href="#" onClick={this.handleLogout}>
                                        Log Out
                                    </NavLink>
                                </NavItem>
                            )}
                        </ul>
                    </Collapse>
                </Navbar>
            </header>
        );
    }
}
