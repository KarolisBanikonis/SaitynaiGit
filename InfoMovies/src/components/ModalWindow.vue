<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div class="modal">
        <section class="modal-body">
          <button type="button" class="btn-close" @click="close">x</button>
          <slot name="body">Nieko nėra.</slot>
       </section>
    </div>
  </div>
  </transition>
</template>

<script>
  export default {
    name: 'ModalWindow',
    methods: {
      close() {
        this.$emit('close');
      },
    },
  };
</script>