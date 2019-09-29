import React, { Component } from 'react';
import { Row, Col, Button, Alert } from 'reactstrap';
import '../../styles/filterStyles.css';

class CustomFilterList extends Component {
    render() {
        return (
            <Row>
                <Col xs='9'>
                    <Alert>{this.props.item.address}</Alert>
                </Col>
                <Col xs='3'>
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
