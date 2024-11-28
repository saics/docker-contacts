<template>
  <form @submit.prevent="handleSubmit">
    <!-- First Name -->
    <div class="mb-3">
      <label for="firstName" class="form-label">{{ $t('firstName') }} *</label>
      <input type="text" id="firstName" v-model="localContact.firstName" class="form-control"
        :class="{ 'is-invalid': errors.firstName }" />
      <div class="invalid-feedback">{{ errors.firstName }}</div>
    </div>

    <!-- Last Name -->
    <div class="mb-3">
      <label for="lastName" class="form-label">{{ $t('lastName') }} *</label>
      <input type="text" id="lastName" v-model="localContact.lastName" class="form-control"
        :class="{ 'is-invalid': errors.lastName }" />
      <div class="invalid-feedback">{{ errors.lastName }}</div>
    </div>

    <!-- Phone Number -->
    <div class="mb-3">
      <label for="phoneNumber" class="form-label">{{ $t('phoneNumber') }} *</label>
      <input type="text" id="phoneNumber" v-model="localContact.phoneNumber" class="form-control"
        :class="{ 'is-invalid': errors.phoneNumber }" />
      <div class="invalid-feedback">{{ errors.phoneNumber }}</div>
    </div>

    <!-- Email -->
    <div class="mb-3">
      <label for="email" class="form-label">{{ $t('email') }}</label>
      <input type="email" id="email" v-model="localContact.email" class="form-control"
        :class="{ 'is-invalid': errors.email }" />
      <div class="invalid-feedback">{{ errors.email }}</div>
    </div>

    <!-- Submit Button -->
    <button type="submit" class="btn btn-primary">
      {{ isEditMode ? $t('saveChanges') : $t('createContact') }}
    </button>
    <button type="button" class="btn btn-danger ms-2" v-if="isEditMode" @click="confirmDelete">
      {{ $t('deleteContact') }}
    </button>
  </form>
</template>

<script>
export default {
  name: 'ContactForm',
  props: {
    contact: {
      type: Object,
      required: true,
    },
    isEditMode: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      localContact: { ...this.contact },
      errors: {},
    };
  },
  watch: {
    contact: {
      handler(newVal) {
        this.localContact = { ...newVal };
      },
      deep: true,
    },
  },
  methods: {
    handleSubmit() {
      this.errors = {};

      if (!this.localContact.firstName) {
        this.errors.firstName = this.$t('firstName') + ' ' + this.$t('isRequired');
      }
      if (!this.localContact.lastName) {
        this.errors.lastName = this.$t('lastName') + ' ' + this.$t('isRequired');
      }
      if (!this.localContact.phoneNumber) {
        this.errors.phoneNumber = this.$t('phoneNumber') + ' ' + this.$t('isRequired');
      }
      if (this.localContact.email && !this.validateEmail(this.localContact.email)) {
        this.errors.email = this.$t('invalidEmailFormat');
      }

      if (Object.keys(this.errors).length === 0) {
        console.log('Submitting localContact:', this.localContact);
        this.$emit('formSubmitted', this.localContact);
      }
    },
    validateEmail(email) {
      const re = /\S+@\S+\.\S+/;
      return re.test(email);
    },
    confirmDelete() {
      if (confirm(this.$t('confirmDelete'))) {
        this.$emit('delete', this.localContact.contactID);
      }
    },
  },
};
</script>
