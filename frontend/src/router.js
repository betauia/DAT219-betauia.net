import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
        routes: [{
            path: '/',
            component: () => import('./views/Intro.vue'),
        },
        {
            path: '/upload',
            component: () => import('./components/Upload/ImageUpload.vue'),
        },
        {
            path: '/image/:id',
            component: () => import('./components/Images/Image.vue'),
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
                path: '/termsofsale',
                component: () => import('./components/Legal/SalgsBetingelserBetaside.vue'),
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
            path: '/events/seatmap/:seatmapid/',
            component: () => import('./components/Events/SeatMapView.vue'),
        },
        {
            path: '/events/seat/:eventid/:seatmapid/:seatid',
            component: () => import('./components/Events/SeatInfo.vue'),
        },
        {
            path: '/ticket/:id',
            component:()=>import('./components/Ticket/BuyTicket.vue'),
        },
        {
            path: '/ticketdetails/:id',
            component:()=>import('./components/Ticket/TicketDetails.vue'),
        },
        {
            path: '/sponsors',
            component: () => import('./components/Sponsors/Sponsors.vue'),
        },
        {
            path: '/sponsors/:id',
            component: () => import('./components/Sponsors/SponsorDetailed.vue'),
        },
//       {
//         path: '/jobs',
//         component: () => import('./components/Jobs/JobView.vue'),
//      },
        {
            path: '/seatmap/:eventid/:seatmapid',
            component: () => import('./components/SeatMap/SeatMapView.vue'),
        },
        {
            path: '/seatmap/:eventid/:seatmapid/:seatid',
            component: () => import('./components/SeatMap/SeatInfo.vue'),
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
          path: '/account/registered',
            component:()=>import('./components/Account/Registered.vue')
        },
        {
            path: '/account/banned',
            component:()=>import('./components/Account/AccountBanned.vue')
        },
        {
            path: '/account/notverified',
            component:()=>import('./components/Account/NotVerified.vue')
        },
        {
            path: '/account/accountdrop',
            component:()=>import('./components/Account/AccountDropdown.vue')
        },
        {
            path: '/account/noaccess',
            component:()=>import('./components/Account/NoAccess.vue')
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
            },
            {
                path: '/account/accountorders',
                component:()=> import('./components/Account/AccountOrders.vue')
            }]
        },
        {
            path: '/resetpassword/:id',
            component:()=> import('./components/Account/ResetPassword.vue')
        },
        {
            path: '/admin/dashboard',
            component: () => import('./components/Admin/AdminPanel.vue'),
            children: [
                {
                    path: '/admin/blog',
                    component: () => import('./components/Admin/Blog.vue'),
                },
                {
                    path: '/admin/dash',
                    component: () => import('./components/Admin/AdminDashboard.vue'),
                },
                {
                    path: '/admin/blog/add',
                    component: () => import('./components/Blog/AddBlogPost.vue'),
                },
                {
                    path: '/admin/blog/:id',
                    component: () => import('./components/Admin/BlogDetail.vue')
                },
                {
                    path: '/admin/event',
                    component: () => import('./components/Admin/Event.vue'),
                },
                {
                    path: '/admin/event/add',
                    component: () => import('./components/Events/AddEvent.vue'),
                },
                {
                    path: '/admin/event/:id',
                    component: () => import('./components/Admin/EventDetail.vue')
                },
                {
                    path: '/admin/eventattendees/:id',
                    component: () => import('./components/Admin/EventAttendees.vue')
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
                    path: '/admin/seatmaps',
                    component: () => import('./components/Admin/SeatMaps.vue'),
                },
                {
                    path: '/admin/seatmapdetail/:id',
                    component: () => import('./components/SeatMap/ViewSeatMap.vue')
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
                    path: '/admin/sponsors',
                    component: () => import('./components/Admin/Sponsors.vue'),
                },
                {
                    path: '/admin/sponsordetail/:id',
                    component: () => import('./components/Admin/SponsorDetail.vue'),
                },
                {
                    path: '/admin/roles',
                    component: () => import('./components/Admin/RolePanel.vue')
                },
                {
                    path: '/admin/roleinfo/:id',
                    component: () => import('./components/Admin/RoleInfo.vue')
                },
                {
                    path: '/admin/addjob',
                    component: () => import('./components/Admin/AddJob.vue')
                },
                {
                    path: '/admin/jobs',
                    component: () => import('./components/Admin/Jobs.vue')
                },
                {
                    path: '/admin/jobdetail/:id',
                    component: () => import('./components/Admin/JobDetail.vue')
                },
                {
                    path:'/admin/ticketverify/:id',
                    component:()=>import('./components/Admin/TicketVerify.vue')
                }
            ]

  },
  {
    path: '/verifyemail/:id',
    component: () => import('./components/Account/EmailVerify.vue'),
  },
  {
      path: '/attendeeemailconfirm/:id',
      component: () => import('./components/Events/ConfirmEventEmail.vue'),
  },
{
  path:'/maintenance',
    component: () => import('./views/maintenance.vue'),
},
  {
    path: '*',
    component: () => import('./components/Error/NotFound.vue'),
  },
  ],
  mode: 'history',
});
