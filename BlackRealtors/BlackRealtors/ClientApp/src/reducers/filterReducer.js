import * as types from '../actions/actionTypes';
import {filterInitState} from '../resources/initialState';

export default function filterReducer(state = filterInitState, action) {
    switch (action.type) {
        case types.ADD_DEFAULT_FILTER:

            let filters = state;
            filters.cocat([action.payload]);

            return {
                ...state,
                state: filters
            }
        case types.DELETE_DEFAULT_FILTER:
            return {
                ...state,
                state: state.filter(filter => filter !== action.payload)
            }
        default:
            return state;
    }
}