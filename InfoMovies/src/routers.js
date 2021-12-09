import SignUp from './components/SignUp.vue'
import Home from './components/Home.vue'
import Login from './components/Login.vue'
import Companies from './components/Companies.vue'
import CreateCompany from './components/CreateCompany.vue'
import UpdateCompany from './components/UpdateCompany.vue'
import Movies from './components/Movies.vue'
import CreateMovie from './components/CreateMovie.vue'
import UpdateMovie from './components/UpdateMovie.vue'
import Comments from './components/Comments.vue'
import CreateComment from './components/CreateComment.vue'
import UpdateComment from './components/UpdateComment.vue'

import { createRouter, createWebHistory } from 'vue-router'

const routes = [
    {
        name: 'SignUp',
        component: SignUp,
        path: '/register'
    },
    {
        name: 'Home',
        component: Home,
        path: '/'
    },
    {
        name: 'Login',
        component: Login,
        path: '/login'
    },
    {
        name: 'Companies',
        component: Companies,
        path: '/companies'
    },
    {
        name:'CreateCompany',
        component: CreateCompany,
        path: '/createcompany'
    },
    {
        name:'UpdateCompany',
        component: UpdateCompany,
        path: '/updatecompany'
    },
    {
        name:'Movies',
        component: Movies,
        path: '/movies'
    },
    {
        name:'CreateMovie',
        component: CreateMovie,
        path: '/createmovie'
    },
    {
        name:'UpdateMovie',
        component: UpdateMovie,
        path: '/updatemovie'
    },
    {
        name:'Comments',
        component: Comments,
        path: '/comments'
    },
    {
        name:'CreateComment',
        component: CreateComment,
        path: '/createcomment'
    },
    {
        name:'UpdateComment',
        component: UpdateComment,
        path: '/updatecomment'
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;