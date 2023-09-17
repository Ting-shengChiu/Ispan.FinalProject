<script setup>
defineProps(["isSelf"]);
</script>

<template>
    <v-row>
        <span class="rounded-lg px-3 py-1 text-white my-1" :class="isSelf ? 'msg-self' : 'msg-remote'"><slot></slot></span>
    </v-row>
</template>

<style scoped>
.msg-remote {
    background-color: gray;
}

.msg-self {
    background-color: #E6A947;
    margin-inline-start: auto;
}
</style>