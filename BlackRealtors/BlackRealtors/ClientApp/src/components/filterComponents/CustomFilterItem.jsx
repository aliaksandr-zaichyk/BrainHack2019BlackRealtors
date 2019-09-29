import React, { Component } from 'react';
import { Row, Col, Button, Alert } from 'reactstrap';
import '../../styles/filterStyles.css';

class CustomFilterList extends Component {
    render() {
        return (
            <Row className='margin-height'>
                <Col xs='8'>{this.props.item.address}</Col>
                <Col xs='4'>
                    <Button
                        outline
                        color='danger'
                        onClick={() => this.props.deleteItem(this.props.item)}
                    >
                        Delete
                    </Button>
                </Col>
            </Row>
        );
    }
}

export default CustomFilterList;
