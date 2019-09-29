import * as types from './actionTypes';
import YandexApi from '../services/Api/YandexApi';

export const addDefaultFilter = (filter) => {
    return { type: types.ADD_DEFAULT_FILTER, payload: filter }
}

export const deleteDefaultFilter = (filter) => {
    return { type: types.DELETE_DEFAULT_FILTER, payload: filter }
}

export const addCustomPointAction = (point) => {
    return { type: types.ADD_CUSTOM_POINT, payload: point }
}

export const deleteCustomPoint = (point) => {
    return { type: types.DELETE_CUSTOM_POINT, payload: point }
}

export const addCustomPoint = address => dispatch => {
    YandexApi
    .getCoordinates(address)
    .then(response => {
        if(response.data.response.GeoObjectCollection.featureMember.length > 1){
            return Error('enter correct data')
        }
        else{
            let point = response.data.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos;

            if(point !== undefined){

                let coordinates = point.split(' ');
                point = {longitude: coordinates[1], latitude: coordinates[0]}

                dispatch(addCustomPointAction(point));

                return point;
            }
            else{
                return Error('Invalid request');
            }
        }
    
    })
    .catch(err => {
        console.log(err);
    })
}