import React, { Component } from 'react';
import {
    YMaps,
    Map,
    FullscreenControl,
    SearchControl,
    ZoomControl,
    Rectangle
} from 'react-yandex-maps';
import { connect } from 'react-redux';
import color from 'color';

class MapComponent extends Component {
    render() {
        const { data } = this.props;
        const r = 0.001859;
        const rr = 0.0014891;

        return (
            <YMaps>
                <Map
                    defaultState={{ center: [53.677834, 23.829529], zoom: 12 }}
                    width='auto'
                    height='80vh'
                >
                    {data &&
                        data.map(
                            ({
                                coordinates: { longitude, latitude },
                                weight
                            }) => (
                                <Rectangle
                                    geometry={[
                                        [longitude - r, latitude - rr],
                                        [longitude + r, latitude + rr]
                                    ]}
                                    options={{
                                        fillColor: color
                                            .rgb(
                                                255 - weight * 255,
                                                weight * 255,
                                                0,
                                                70
                                            )
                                            .hex(),
                                        opacity: 0.7,
                                        outline: false
                                    }}
                                />
                            )
                        )}
                </Map>
            </YMaps>
        );
    }
}

const mapStateToProps = state => ({
    data: state.heatMapData
});

export default connect(mapStateToProps)(MapComponent);
