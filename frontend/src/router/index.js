import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import EditContactView from '../views/EditContactView.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView,
  },
  {
    path: '/edit',
    name: 'AddContact',
    component: EditContactView,
  },
  {
    path: '/edit/:id',
    name: 'EditContact',
    component: EditContactView,
    props: true,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
