import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      component: () => import(/* webpackChunkName: "start" */ './views/Intro.vue'),
    },
    {
      path: '/privacy',
      component: () => import(/* webpackChunkName: "privacy" */ './components/PrivacyPolicy.vue'),
    },
    {
      path: '/disclaimer',
      component: () => import(/* webpackChunkName: "disclaimer" */ './components/Disclaimer.vue'),
    },
    {
      path: '/blog',
      component: () => import(/* webpackChunkName: "disclaimer" */ './components/Blog.vue'),
    },
    {
      path: '/login',
      component: () => import(/* webpackChunkName: "login" */ './components/_LoginModal.vue'),
    },
    {
      path: '*',
      component: () => import(/* webpackChunkName: "about" */ './components/NotFound.vue'),
    },
  ],
});
