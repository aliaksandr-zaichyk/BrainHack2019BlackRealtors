import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import pointReducer from '../reducers/pointReducer';
import handleReducer from '../reducers/handleReducer';

export default function configureStore (initialState) {

  const middleware = [
    thunk
  ];

  const enhancers = [];
  
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    points: pointReducer,
    heatMapData: handleReducer

  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
