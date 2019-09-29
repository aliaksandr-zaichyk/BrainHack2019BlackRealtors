import axios from 'axios';
import {YANDEX_URL} from '../../resources/urls';

class YandexApi {
    
    static getCoordinates(address) {
        return axios
            .get(YANDEX_URL, {params:{geocode: address, lang: 'ru_RU', format: 'json'}});
    }
}

export default YandexApi;
