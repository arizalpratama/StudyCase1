# StudyCase1
Microservices REST API

Connection String:
- Use 'Local' for development
- Use 'Database' for container

Docker Build and Push:
- docker build -t arizalpratama01/authserver .
- - docker push arizalpratama01/authserver
- docker build -t arizalpratama01/enrollmentservice .
- docker push arizalpratama01/enrollmentservice
- docker build -t arizalpratama01/paymentservice .
- docker push arizalpratama01/paymentservice

Kubernetes:
- kubectl get deployments
- kubectl get services
- kubectl get pods

Kubernetes Apply:
- kubectl apply -f enrollments-depl.yaml
- kubectl apply -f payments-depl.yaml
- kubectl apply -f ingress-srv.yaml
- kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.0/deploy/static/provider/cloud/deploy.yaml
- kubectl apply local-pvc.yaml
- kubectl apply -f mssql-plat-depl.yaml


Kubernetes Delete:
- kubectl delete deployment enrollments-depl
- kubectl delete deployment payments-depl
- kubectl delete deployment ingress-srv
- kubectl delete deployment local-pvc
- kubectl delete deployment mssql-depl
