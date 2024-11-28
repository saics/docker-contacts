<template>
  <div class="container mt-4">
    <h1>{{ isEditMode ? $t('editContact') : $t('addNewContact') }}</h1>
    <contact-form
      :contact="contact"
      :is-edit-mode="isEditMode"
      @formSubmitted="handleSubmit"
      @delete="handleDelete"
    >
    </contact-form>
  </div>
</template>

<script>
import apiClient from '@/api/axios';
import ContactForm from '../components/ContactForm.vue';

export default {
  name: 'EditContactView',
  components: {
    ContactForm,
  },
  data() {
    return {
      contact: {
        contactID: null,
        firstName: '',
        lastName: '',
        phoneNumber: '',
        email: '',
      },
      isEditMode: false,
    };
  },
  methods: {
    async fetchContact() {
      try {
        const response = await apiClient.get(
          `/Contacts/${this.contact.contactID}`
        );
        this.contact = response.data;
      } catch (error) {
        console.error('Error fetching contact:', error);
        alert(this.$t('errorOccurred'));
      }
    },
    async handleSubmit(contact) {
      try {
        if (this.isEditMode) {
          //         existing contact
          await apiClient.put(
            `/Contacts/${contact.contactID}`,
            contact
          );
          alert(this.$t('contactUpdated'));
        } else {
          //               new contact
          await apiClient.post('/Contacts', contact);
          alert(this.$t('contactCreated'));
        }
        this.$router.push({ name: 'Home' });
      } catch (error) {
        console.error('Error saving contact:', error);
        alert(this.$t('errorOccurred'));
        return;
      }
    },
    async handleDelete(contactID) {
      try {
        await apiClient.delete(`/Contacts/${contactID}`);
        alert(this.$t('contactDeleted'));
        this.$router.push({ name: 'Home' });
      } catch (error) {
        console.error('Error deleting contact:', error);
        alert(this.$t('errorOccurred'));
      }
    },
  },
  created() {
    const contactID = this.$route.params.id;
    if (contactID) {
      this.contact.contactID = parseInt(contactID);
      this.isEditMode = true;
      this.fetchContact();
    }
  },
};
</script>
