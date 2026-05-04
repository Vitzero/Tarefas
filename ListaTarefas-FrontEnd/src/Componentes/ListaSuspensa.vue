<script setup>
import Tarefa from "./Tarefa.vue";

defineProps({ tarefas: Array });
defineEmits(["editar", "deletar"]);
</script>

<template>
	<div class="Lista">
		<p v-if="!tarefas?.length" class="vazio">Nenhuma tarefa encontrada.</p>
		<Tarefa
			v-for="tarefa in tarefas"
			:key="tarefa.id"
			:titulo="tarefa.titulo"
			:descricao="tarefa.descricao"
			:status="tarefa.status"
			:dataDeCriacao="tarefa.dataDeCriacao"
			:data="tarefa.data"
			@editar="$emit('editar', tarefa)"
			@deletar="$emit('deletar', tarefa.id)"
		/>
	</div>
</template>

<style scoped>
.Lista {
	display: flex;
	flex-direction: column;
	justify-content: flex-start;
	align-items: center;
	gap: 12px;
	padding: 32px 16px;
	height: 100%;
	background-color: var(--gray-600);
}

.vazio {
	flex: 1;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
	gap: 16px;
	color: var(--gray-300);
	font-size: 0.875rem;
	font-weight: 600;
}
</style>
