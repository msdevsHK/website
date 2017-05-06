#!/usr/bin/env bash

# https://gist.github.com/stvvt/65babbf7b49858470028da62eb1bec95

set -eu

# PATH TO YOUR HOSTS FILE
: ${ETC_HOSTS="/etc/hosts"}

# DEFAULT IP FOR HOSTNAME
DEFAULT_IP="127.0.0.1"

VERBOSE=false

function remove() {
    local HOSTNAME=$1
    local HOST_REGEX="\(\s\+\)${HOSTNAME}\s*$"
    local HOST_LINE="$(grep -e "${HOST_REGEX}" ${ETC_HOSTS})"
    
    if [ -n "${HOST_LINE}" ]; then
        [ ${VERBOSE} == true ] && echo "${HOSTNAME} Found in your ${ETC_HOSTS}, Removing now..."
        sed -i -e "s/${HOST_REGEX}/\1/g" -e "/^[^#][0-9\.]\+\s\+$/d" ${ETC_HOSTS}
    else
        [ ${VERBOSE} == true ] && echo "${HOSTNAME} was not found in your ${ETC_HOSTS}";
    fi
}

function add() {
    local HOSTNAME=$1
    local IP=${2:-${DEFAULT_IP}}

    local HOST_REGEX="\(\s\+\)${HOSTNAME}\s*$"
    local HOST_LINE="$(grep -e "${HOST_REGEX}" ${ETC_HOSTS})"
    
    if [ -n "${HOST_LINE}" ]; then
        [ ${VERBOSE} == true ] && echo "${HOSTNAME} already exists : ${HOST_LINE}"
    else
        [ ${VERBOSE} == true ] && echo "Adding ${HOSTNAME} to your ${ETC_HOSTS}";
        echo -e "${IP}\t${HOSTNAME}" >> ${ETC_HOSTS}
        [ ${VERBOSE} == true ] && echo -e "${HOSTNAME} was added succesfully \n ${HOST_LINE}";
    fi
}

# Execute
$@