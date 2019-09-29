import React, { Component } from 'react';
import {
    Row,
    Col,
    Card,
    CardFooter,
    CardBody,
    Button,
    Container
} from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import Map from './mapComponents/MapComponent';
import FilterPanel from './filterComponents/FilterPanel';
import { sendData } from '../actions/actions';

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

        this.props.sendData(sendObject);
    };

    render() {
        return (
            <Container fluid>
                <Row>
                    <Col md={6} xs={12}>
                        <Card>
                            <CardBody>
                                <Map />
                            </CardBody>
                        </Card>
                    </Col>
                    <Col md={6} xs={12}>
                        <Card>
                            <CardBody>
                                <FilterPanel />
                            </CardBody>
                            <CardFooter>
                                <Button onClick={this.sendData}>SEND</Button>
                            </CardFooter>
                        </Card>
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
    return bindActionCreators(
        {
            sendData: data => sendData(data)
        },
        dispatch
    );
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MainComponent);
