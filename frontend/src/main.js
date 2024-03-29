import Vue from 'vue';
import Buefy from 'buefy';
import Vuex from 'vuex';
import Vuetify from 'vuetify';
import Datetime from 'vue-datetime';
import 'vue-datetime/dist/vue-datetime.css';

import 'buefy/dist/buefy.css';
import '../node_modules/bulma/css/bulma.css';
import 'vuetify/dist/vuetify.min.css';

import App from './App.vue';
import router from './router';

import imageWidget from './components/Images/ImageWidget.vue';

Vue.config.productionTip = false;

Vue.use(Buefy);
Vue.use(Vuex);
Vue.use(Vuetify);
Vue.use(Datetime);
Vue.component('imageWidget',imageWidget);

Vue.config.errorHandler = function (err, vm, info) {
    console.log(err)
    console.log(vm)
    console.log(info)
};

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
