# StudyCase1
Microservices REST API

Docker:
- docker build -t arizalpratama01/enrollmentservice .
- docker push arizalpratama01/enrollmentservice
- docker build -t arizalpratama01/paymentservice .
- docker push arizalpratama01/paymentservice

Kubernetes:
- kubectl get deployments
- kubectl get services
- kubectl get pods

Kubernetes Apply:
- Kubectl apply -f enrollments-depl.yaml
- Kubectl apply -f payments-depl.yaml
- Kubectl apply local-pvc.yaml
- Kubectl apply -f mssql-plat-depl.yaml
- Kubectl apply -f ingress-srv.yaml
- Kubectl apply -f rabbitmq-depl.yaml

Kubernetes Delete:
- kubectl delete deployment platforms-depl
- kubectl delete deployment commands-depl
- kubectl delete deployment mssql-depl
- kubectl delete deployment rabbitmq-depl
- kubectl delete local-pvc
- kubectl delete deployment ingress-srv
