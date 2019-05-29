import Vue from 'vue';
import Buefy from 'buefy';
import Vuex from 'vuex';

import 'buefy/dist/buefy.css';
import '../node_modules/bulma/css/bulma.css';

import App from './App.vue';
import router from './router';

import axios from 'axios';

Vue.config.productionTip = false;
axios.defaults.baseURL = 'http://localhost:5001/';
axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';

Vue.use(Buefy);
Vue.use(Vuex);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
