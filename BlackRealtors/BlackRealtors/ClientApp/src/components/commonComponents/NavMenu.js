import React, {Component} from 'react';
import { Container, Navbar, NavbarBrand} from 'reactstrap';
import { Link } from 'react-router-dom';

export default class NavMenu extends Component {
  render() {
    return (
      <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
        <Container>
          <NavbarBrand tag={Link} to="/">BlackRealtors</NavbarBrand>
        </Container>
      </Navbar>
    );
  }
}
