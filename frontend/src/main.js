import Vue from 'vue';
import Buefy from 'buefy';
import Vuex from 'vuex';

import 'buefy/dist/buefy.css';
import '../node_modules/bulma/css/bulma.css';

import App from './App.vue';
import router from './router';


Vue.config.productionTip = false;
Vue.use(Buefy);
Vue.use(Vuex);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
