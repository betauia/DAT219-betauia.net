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
            component: () => import('./components/Legal/PrivacyPolicy.vue'),
        },
        {
            path: '/disclaimer',
            component: () => import('./components/Legal/Disclaimer.vue'),
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
            path: '/events/:id',
            component: () => import('./components/Events/EventDetailed.vue'),
        },
        {
            path: '/events',
            component: () => import('./views/Events.vue'),
        },
        {
            path: '/sponsors',
            component: () => import('./components/Sponsors/Sponsors.vue'),
        },
        {
            path: '/sponsors/:id',
            component: () => import('./components/Sponsors/SponsorDetailed.vue'),
        },
        {
            path: '/seatmap/:id',
            component: () => import('./components/SeatMap/SeatMapView.vue'),
        },
        {
            path: '/account/login',
            component: () => import('./components/Account/Login.vue'),
        },
        {
            path: '/account/logout',
            component: () => import('./components/Account/Logout.vue'),
        },
        {
            path: '/account/register',
            component: () => import('./components/Account/Register.vue'),
        },
        {
            path: '/account',
            component: () => import('./components/Account/Account.vue'),
            children: [{
                    path: '/account/info',
                    component: () => import('./components/Account/AccountInfo.vue'),
                },
                {
                    path: '/account/edit/:id',
                    component: () => import('./components/Account/EditAccount.vue'),
                }
            ]
        },
        {
            path: '/admin/dashboard',
            component: () => import('./components/Admin/AdminPanel.vue'),
            children: [{
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
                    path: '/admin/seatmapeditor',
                    component: () => import('./components/Admin/SeatMapEditor.vue'),
                },
                {
                    path: '/admin/userinfo/:id',
                    component: () => import('./components/Admin/UserInfoPanel.vue'),
                },
                {
                    path: '/admin/addsponsor',
                    component: () => import('./components/Admin/AddSponsors.vue'),
                },
                {
                    path: '/admin/roles',
                    component: () => import('./components/Admin/RolePanel.vue')
                },
                {
                    path: '/admin/roleinfo/:id',
                    component: () => import('./components/Admin/RoleInfo.vue')
                }
            ]

        },
        {
            path: '/verifyemail/:id',
            component: () => import('./components/Account/EmailVerify.vue')
        },
        {
            path: '*',
            component: () => import('./components/Error/NotFound.vue'),
        }
    ],
    mode: 'history',
});
