import React, { Component } from 'react';
import { Row, Col, Alert, Input, Button } from 'reactstrap';
import { priorities } from '../../resources/filterData';
import '../../styles/filterStyles.css';

class DefaultFilterItem extends Component {
    constructor(props) {
        super(props);

        this.state = {
            priorities: priorities,
            selectValue: priorities[0]
        };
    }

    handleChange = event => {
        this.setState({
            selectValue: event.target.value
        });
    };

    render() {
        return (
            <Row>
                <Col xs={4}>
                    <Input id='filter' type='hidden' value={this.props.item} />
                    {this.props.item}
                </Col>
                <Col xs={4}>
                    <Input
                        id='priority'
                        type='select'
                        value={this.state.selectValue}
                        onChange={this.handleChange}
                    >
                        {this.state.priorities.map((priority, index) => (
                            <option key={index}>{priority}</option>
                        ))}
                    </Input>
                </Col>
                <Col xs={4}>
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

export default DefaultFilterItem;
