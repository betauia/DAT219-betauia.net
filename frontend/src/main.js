import Vue from 'vue';
import Buefy from 'buefy';
import Vuex from 'vuex';

import Datetime from 'vue-datetime';
import 'vue-datetime/dist/vue-datetime.css';

import 'buefy/dist/buefy.css';
import '../node_modules/bulma/css/bulma.css';

import vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';

import App from './App.vue';
import router from './router';

import imageWidget from './components/Images/ImageWidget';

Vue.config.productionTip = false;

Vue.use(Buefy);
Vue.use(Vuex);
Vue.use(vuetify);

Vue.use(Datetime);
Vue.component('imageWidget',imageWidget);

new Vue({
  router,
  vuetify,
  render: h => h(App),
}).$mount('#app');
