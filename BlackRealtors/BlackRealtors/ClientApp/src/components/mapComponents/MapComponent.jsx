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
                    {data.length !== 0 && (
                        <Rectangle
                            geometry={[
                                [53.610826, 23.777779],
                                [53.718562, 23.864186]
                            ]}
                            options={{
                                fillImageHref: '/images/final_map.png',
                                outline: false
                            }}
                        />
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
