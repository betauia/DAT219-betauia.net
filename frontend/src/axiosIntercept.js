import router from './router'
import axios from 'axios'

axios.interceptors.response.use(function (response){
    return response;
},function (error) {
    console.log(error.response);
    if(error.response.status === 401){
        router.push("/noe")
    }
    return Promise.reject((error))
});

export default axios;
