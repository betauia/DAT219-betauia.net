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
      component: () => import(/* webpackChunkName: "blog" */ './views/Blog.vue'),
    },
    {
      path: '/events',
      component: () => import(/* webpackChunkName: "events" */ './views/Events.vue'),
    },
    {
      path: '/account/login',
      component: () => import(/* webpackChunkName: "login" */ './components/Account/Login.vue'),
    },
    {
      path: '/account/register',
      component: () => import(/* webpackChunkName: "register" */ './components/Account/Register.vue'),
    },
    {
      path: '/test',
      component: () => import(/* webpackChunkName: "test" */ './views/Test.vue'),
    },
    {
      path: '*',
      component: () => import(/* webpackChunkName: "about" */ './components/NotFound.vue'),
    },
  ],
  mode: 'history',
});
