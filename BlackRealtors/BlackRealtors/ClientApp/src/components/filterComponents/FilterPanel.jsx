import React, { Component } from 'react';
import DefaultFilterList from './DefaultFilterList';
import CustomFilterList from './CustomFilterList';
import { Row, Col } from 'reactstrap';

class FilterPanel extends Component {
    render() {
        return (
            <Row>
                <Col sm={6}>
                    <CustomFilterList />
                </Col>
                <Col sm={6}>
                    <DefaultFilterList />
                </Col>
            </Row>
        );
    }
}

export default FilterPanel;
