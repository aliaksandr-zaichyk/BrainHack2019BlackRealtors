import React, { Component } from 'react';
import {
  YMaps,
  Map,
  FullscreenControl,
  SearchControl,
  ZoomControl
} from 'react-yandex-maps';
import { Button } from 'reactstrap';
import YandexApi from '../../services/Api/YandexApi';

class MapComponent extends Component {

  getValue = () => {
    YandexApi
    .getCoordinates('ул.Дзержинского 25, Беларусь, Гродно')
    .then(point =>
      {
        let p = point.data.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos;
        let arr = p.split(' ');
        p = `${arr[1]}, ${arr[0]}`
        console.log(point.data.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos);
        console.log(p);
      }
    );
  }

  render() {
    return (
      <div>
        <YMaps>
          <Map defaultState={{ center: [53.677834, 23.829529], zoom: 12 }} width={640} height={640}>
            <FullscreenControl />
            <div id='nice'>
              <SearchControl options={{ float: 'right' }} />
            </div>
            <ZoomControl options={{ float: 'right' }} />
          </Map>
        </YMaps>
        <Button
          onClick={this.getValue}
        >Load</Button>
      </div>
    );
  }
}

export default MapComponent;