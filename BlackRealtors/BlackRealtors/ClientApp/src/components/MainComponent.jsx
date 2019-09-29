import React, { Component } from 'react';
import { Row, Col, Card, CardBody, Button, Container } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import Map from './mapComponents/MapComponent';
import FilterPanel from './filterComponents/FilterPanel';
import HandleApi from '../services/Api/HandleApi';

class MainComponent extends Component {
    sendData = () => {
        let filters = [];
        let points = this.props.points;
        let filterNames = document.querySelectorAll('[id=filter]');
        let priorities = document.querySelectorAll('[id=priority]');

        for (let i = 0; i < filterNames.length; i++) {
            filters.push({
                organizationType: filterNames[i].value,
                importanceLevel: priorities[i].value
            });
        }

        let sendObject = {
            defaultFilters: filters,
            customPoints: points
        };

        HandleApi.sendData(sendObject);
    };

    render() {
        return (
            <Container>
                <Row>
                    <Col sm={6} xs={12}>
                        <Card>
                            <CardBody>
                                <Map />
                            </CardBody>
                        </Card>
                    </Col>
                    <Col sm={6} xs={12}>
                        <Row>
                            <FilterPanel />
                            <Button onClick={this.sendData}>SEND</Button>
                        </Row>
                    </Col>
                </Row>
            </Container>
        );
    }
}

const mapStateToProps = state => {
    return {
        points: state.points
    };
};

const mapDispatchToProps = dispatch => {
    return bindActionCreators({}, dispatch);
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MainComponent);
