# create specific service account for Deploy platform
gcloud iam service-accounts create sa001-azuredevops --display-name="azuredevops-pipeline" \
    --description="Account to deploy resources on GCP from Azure Devops" 

# settings roles: see https://cloud.google.com/run/docs/reference/iam/roles#additional-configuration
AZURE_PIPELINES_PUBLISHER=sa001-azuredevops@$GOOGLE_CLOUD_PROJECT.iam.gserviceaccount.com

gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/storage.admin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/storage.objectAdmin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/storage.objectCreator
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/pubsub.admin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/run.admin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role roles/iam.serviceAccountUser
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/artifactregistry.admin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/compute.admin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/compute.loadBalancerAdmin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/compute.networkAdmin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/compute.orgFirewallPolicyAdmin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/compute.securityAdmin
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/editor
gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/iam.securityAdmin
  
# gcloud run services add-iam-policy-binding gcr06d1gwe11798-metricssbl --platform managed --region=europe-west1 \
#    --member=serviceAccount:scheduler-runner@gpjegld01-1798-ddl-app.iam.gserviceaccount.com \
#    --role=roles/run.invoker

gcloud projects add-iam-policy-binding $GOOGLE_CLOUD_PROJECT --member=serviceAccount:$AZURE_PIPELINES_PUBLISHER \
    --role=roles/editor