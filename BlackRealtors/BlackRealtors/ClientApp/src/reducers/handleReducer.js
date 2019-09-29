import * as types from '../actions/actionTypes';
import {handleInitState} from '../resources/initialState';

export default function pointReducer(state = handleInitState, action) {
    switch (action.type) {
        case types.GET_DATA_FROM_API:
            state = action.payload;
            return state;

        default:
            return state;
    }
}