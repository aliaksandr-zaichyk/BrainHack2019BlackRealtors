import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import filterReducer from '../reducers/filterReducer';
import pointReducer from '../reducers/pointReducer';

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
    filters: filterReducer,
    points: pointReducer  
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
