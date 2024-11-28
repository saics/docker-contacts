<template>
  <div class="container mt-4">
    <h1>{{ $t('contactList') }}</h1>
    <router-link to="/edit" class="btn btn-success mb-3">{{ $t('addNewContact') }}</router-link>
    <contact-list></contact-list>
  </div>
</template>

<script>
import ContactList from '../components/ContactList.vue';

export default {
  name: 'HomeView',
  components: {
    ContactList,
  },
};
</script>
