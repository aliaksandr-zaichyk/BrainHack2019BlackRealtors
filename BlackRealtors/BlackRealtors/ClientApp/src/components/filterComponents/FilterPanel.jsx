import React, { Component } from 'react';
import DefaultFilterList from './DefaultFilterList';
import CustomFilterList from './CustomFilterList';
import { Row, Col } from 'reactstrap';

class FilterPanel extends Component {
    render() {
        return (
            <Row>
                <Col xs={12} xl={6}>
                    <CustomFilterList />
                </Col>
                <Col xs={12} xl={6}>
                    <DefaultFilterList />
                </Col>
            </Row>
        );
    }
}

export default FilterPanel;
