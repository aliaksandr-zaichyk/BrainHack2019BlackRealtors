import React from 'react';
import { Route } from 'react-router';
import Layout from './components/commonComponents/Layout';
import MainComponent from './components/MainComponent';

export default () => (
  <Layout>
    <Route exact path='/' component={MainComponent} />
  </Layout>
);