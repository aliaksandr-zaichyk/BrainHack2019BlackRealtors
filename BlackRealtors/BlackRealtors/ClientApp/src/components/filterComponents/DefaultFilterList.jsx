import React, { Component } from 'react';
import { defaultFilters } from '../../resources/filterData';
import DefaultFilterItem from './DefaultFilterItem';
import {
  Row,
  Col,
  Button,
  Input,
  Card,
  Alert
} from 'reactstrap';
import '../../styles/filterStyles.css';

class DefaultFilterList extends Component {

  constructor(props) {
    super(props);

    this.state = {
      filters: defaultFilters,
      items: [],
      selectValue: defaultFilters[0]
    }
  }

  handleChange = (event) => {
    this.setState({
      selectValue: event.target.value
    });
  }

  addFilter = () => {
    this.setState({
      items: this.state.items.concat([this.state.selectValue]),
      selectValue: document.querySelectorAll('[id=option]')[1].value
    });
  }

  deleteFilter = (item) => {
    this.setState({
      items: this.state.items.filter((filterItem) => filterItem !== item)
    })
  }

  render() {
    return (
      <Card>
        <Alert color='danger'>
          <h4>Default Filters</h4>
        </Alert>
        {this.state.items.map((item, index) =>
          <DefaultFilterItem key={index} item={item} deleteItem={this.deleteFilter} />
        )}
        <Row>
          <Col xs='9'>
            <Input
              type="select"
              value={this.state.selectValue}
              onChange={this.handleChange}
              className='input-size'
            >
              {this.state.filters.filter(x => !this.state.items.find(y => y === x)).map((filter, index) =>
                <option id='option' key={index}>{filter}</option>
              )}
            </Input>
          </Col>
          <Col xs='3'>
            <Button
              outline
              color='success'
              className='add-button'
              onClick={this.addFilter}
            >Add Filter</Button>
          </Col>
        </Row>
      </Card>
    );
  }
}

export default DefaultFilterList;