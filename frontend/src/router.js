import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  routes: [{
      path: '/',
      component: () => import('./views/Intro.vue'),
    },
    {
      path: '/privacy',
      component: () => import('./components/PrivacyPolicy.vue'),
    },
    {
      path: '/disclaimer',
      component: () => import('./components/Disclaimer.vue'),
    },
    {
      path: '/blog',
      component: () => import('./views/Blog.vue'),
    },
    {
      path: '/blog/detailed/:id',
      component: () => import('./components/Blog/BlogDetailed.vue'),
    },
    {
      path: '/events',
      component: () => import('./views/Events.vue'),
    },
    {
      path: '/account/login',
      component: () => import('./components/Account/Login.vue'),
    },
    {
      path: '/account/register',
      component: () => import('./components/Account/Register.vue'),
    },
    {
      path: '/account/info',
      component: () => import('./components/Account/AccountInfo.vue'),
    },
    {
      path: '/admin/dashboard',
      component: () => import('./components/Admin/AdminPanel.vue'),
      children: [
        {
          path: '/blog/add',
          component: () => import('./components/Blog/AddBlogPost.vue'),
        },
        {
          path: '/event/add',
          component: () => import('./components/Events/AddEvent.vue'),
        },
        {
          path: '/admin/users',
          component: () => import('./components/Admin/UserPanel.vue'),
        },
        {
          path: '/admin/userinfo',
          component: () => import('./components/Admin/UserInfoPanel.vue'),
        }
      ]

    },
    {
      path: '/verifyemail/:id',
      component: () => import('./components/Account/EmailVerify.vue')
    },
    {
      path: '*',
      component: () => import('./components/NotFound.vue'),
    }
  ],
  mode: 'history',
});

