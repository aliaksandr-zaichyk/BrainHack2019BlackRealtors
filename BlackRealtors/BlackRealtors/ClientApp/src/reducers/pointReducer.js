import * as types from '../actions/actionTypes';
import { pointInitState } from '../resources/initialState';

export default function pointReducer(state = pointInitState, action) {
    switch (action.type) {
        case types.ADD_CUSTOM_POINT:
            state.push(action.payload);

            return state;
        case types.DELETE_CUSTOM_POINT:
            return {
                ...state,
                state: state.filter(point => point !== action.payload)
            };
        default:
            return state;
    }
}
