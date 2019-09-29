import React, { Component } from 'react';
import { Row, Col, Card, Button } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import Map from './mapComponents/MapComponent';
import FilterPanel from './filterComponents/FilterPanel';
import {sendData} from '../actions/actions';

class MainComponent extends Component {

  sendData = () => {
    let filters = []
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
    HandleApi.sendData(sendObject);
  }

  render() {
    return (
      <Row>
        <Col xs={5} >
          <Col xs={6}>
          </Col>
          <Col xs={6}>
            <Card>
              <Map />
            </Card>
          </Col>
        </Col>
        <Col xs={7}>
          <FilterPanel />
          <Button
            onClick={this.sendData}
          >SEND</Button>
        </Col>
      </Row>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    points: state.points
  }
}

const mapDispatchToProps = (dispatch) => {
  return bindActionCreators({
    sendData: (data) => sendData(data)
  }, dispatch)
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MainComponent);

export default connect(mapStateToProps, mapDispatchToProps)(MainComponent);