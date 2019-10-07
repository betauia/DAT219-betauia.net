import router from './router'
import axios from 'axios'

axios.interceptors.response.use(function (response){
    return response;
},function (error) {
    if(error.response.status === 401){
        router.push("/account/login")
    }else if(error.response.data === 602){
        router.push("/account/banned")
    }else if(error.response.status ===500){
        router.push("/maintenance")
    }
    return Promise.reject((error))
});

export default axios;
