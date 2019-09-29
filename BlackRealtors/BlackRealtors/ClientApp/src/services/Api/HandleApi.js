import axios from 'axios';
import { BASE_SEARCH_URL } from '../../resources/urls';

class HandleApi {
    static sendData(data) {
        return axios
            .post(BASE_SEARCH_URL, data)
            .then(res => {
                console.log(res.data);
            })
            .catch(err => {
                console.log(err);
            });
    }
}

export default HandleApi;
