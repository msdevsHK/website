#!/bin/bash
# create.sh
: ${1?"Missing hostname. Usage: $0 HOSTNAME PFXPASSWORD"}
: ${2?"Missing password. Usage: $0 HOSTNAME PFXPASSWORD"}

# http://stackoverflow.com/questions/10175812/how-to-create-a-self-signed-certificate-with-openssl
# https://security.stackexchange.com/questions/74345/provide-subjectaltname-to-openssl-directly-on-command-line
# http://stackoverflow.com/questions/21488845/how-can-i-generate-a-self-signed-certificate-with-subjectaltname-using-openssl

CERTS_DIRECTORY=../../MSDevsHK.Website/certs

# Create a self-signed CA certificate, and add the SAN name to work in latest Chrome browser.
openssl req -x509 -nodes -sha256 \
    -newkey rsa:2048 \
    -keyout $CERTS_DIRECTORY/privatekey.key \
    -out $CERTS_DIRECTORY/cert.crt \
    -days 365 \
    -subj "/CN=$1" \
    -config <(cat /etc/ssl/openssl.cnf \
        <(printf "[req]\nx509_extensions=x509_ext\n[x509_ext]\nextendedKeyUsage=serverAuth\nsubjectAltName=DNS:$1"))

    #-extensions san_env \
# Export the cert with private key to a PFX file.
openssl pkcs12 -export \
    -inkey $CERTS_DIRECTORY/privatekey.key \
    -in $CERTS_DIRECTORY/cert.crt \
    -out $CERTS_DIRECTORY/cert.pfx \
    -password pass:$2

# Remove the plaintext private key now it is stored in the PFX file, protected with the password.
rm $CERTS_DIRECTORY/privatekey.key
